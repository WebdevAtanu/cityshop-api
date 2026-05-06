-- CREATE TYPE OrderItemType as TABLE (
--     ItemId UNIQUEIDENTIFIER,
--     SizeId UNIQUEIDENTIFIER,
--     ItemQuantity INT
-- )

-- CREATE type CustomerType as table(
--     CustomerName varchar(50),
--     CustomerEmail varchar(50),
--     CustomerPhone varchar(50),
--     CustomerAddress varchar(200),
--     Pincode varchar(50)
-- )

GO
ALTER FUNCTION GenerateOrderNumber(
    @ShopId uniqueidentifier,
    @ShopCode varchar(50)
) 
returns VARCHAR(50)
AS
BEGIN

    DECLARE @LastOrderNumber VARCHAR(50);
    DECLARE @LastSeries VARCHAR(50);
    DECLARE @NewSeries INT;
    DECLARE @OrderNumber VARCHAR(50);

    -- get last order number
    select top 1
        @LastOrderNumber= OrderNumber
    from Tbl_Shop_Order
    where shopId = @ShopId
    ORDER BY CAST(RIGHT(OrderNumber,4) AS INT) DESC

    IF @LastOrderNumber IS NULL
    BEGIN
        SET @NewSeries = 1;
    END
    ELSE
    BEGIN
        Set @LastSeries = CAST(RIGHT(@LastOrderNumber,4) AS INT);
        SET @NewSeries = @LastSeries + 1;
    END
    SET @OrderNumber = @ShopCode + '-' + RIGHT(REPLICATE('0',4)+CAST(@NewSeries as VARCHAR),4);

    RETURN @OrderNumber
END

GO
ALTER PROCEDURE sp_InsertOrder(
    @ShopId uniqueidentifier,
    @Customer CustomerType READONLY,
    @Items OrderItemType READONLY
)
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        BEGIN TRANSACTION

        DECLARE @CustomerId UNIQUEIDENTIFIER=newid();
        DECLARE @OrderId UNIQUEIDENTIFIER=newid();
        DECLARE @ShopCode VARCHAR(50);

        -- get shop code
        SELECT @ShopCode = ShopCode
    FROM M_Shop
    WHERE ShopId = @ShopId
    
        -- insert customer
        INSERT into M_Customer
        (
        CustomerId,
        CustomerName,
        CustomerPhone,
        CustomerEmail,
        CustomerAddress,
        Pincode,
        CreatedDate,
        IsActive
        )

    SELECT
        @CustomerId,
        CustomerName,
        CustomerPhone,
        CustomerEmail,
        CustomerAddress,
        Pincode,
        GETDATE(),
        1
    FROM @Customer

        -- insert order
        INSERT INTO Tbl_Shop_Order
        (
        OrderId,
        ShopId,
        OrderNumber,
        CustomerId,
        DeliveryAddress,
        Pincode,
        Phone,
        CreatedDate,
        IsActive
        )
    SELECT
        @OrderId,
        @ShopId,
        dbo.GenerateOrderNumber(@ShopId, @ShopCode),
        @CustomerId,
        CustomerAddress,
        Pincode,
        CustomerPhone,
        GETDATE(),
        1
    FROM @Customer

        -- Insert Order Items
        INSERT INTO Map_Order_Items
        (
        MapId,
        OrderId,
        ItemId,
        SizeId,
        ItemQty,
        CreatedDate,
        IsActive
        )
    SELECT
        NEWID(),
        @OrderId,
        ItemId,
        SizeId,
        ItemQuantity,
        GETDATE(),
        1
    from @Items

        COMMIT TRANSACTION
        END TRY
    
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
        END CATCH
END