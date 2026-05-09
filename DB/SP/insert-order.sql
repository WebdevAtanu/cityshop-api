-- ========================= order items type ==============================
CREATE TYPE OrderItemType AS TABLE (
	ItemId uniqueidentifier NULL,
	SizeId uniqueidentifier NULL,
	ItemQuantity int NULL
);

-- ========================= customer type ==============================
CREATE TYPE CustomerType AS TABLE (
	CustomerName varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerEmail varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerPhone varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CustomerAddress varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Pincode varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
);

-- ========================= get item price function ==============================
GO
CREATE   FUNCTION fn_GetItemPrice(
    @ShopId UNIQUEIDENTIFIER, 
    @ItemId UNIQUEIDENTIFIER,
    @ItemSizeId UNIQUEIDENTIFIER,
    @ItemQty INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Price DECIMAL(18,2);

    SELECT TOP 1 @Price = Price
    FROM map_shop_item
    WHERE ShopId = @ShopId 
      AND ItemId = @ItemId 
      AND SizeId = @ItemSizeId;

    RETURN ISNULL(@Price * @ItemQty, 0);
END;

-- ========================= generating order number function ==============================
GO
CREATE FUNCTION GenerateOrderNumber(
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
END;

-- ========================= SP for inserting new order ==============================
GO
CREATE PROCEDURE [dbo].[sp_InsertOrder]
(
    @ShopId UNIQUEIDENTIFIER,
    @Customer CustomerType READONLY,
    @Items OrderItemType READONLY,
    @OrderNumber VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @CustomerId UNIQUEIDENTIFIER = NEWID();
        DECLARE @OrderId UNIQUEIDENTIFIER = NEWID();
        DECLARE @ShopCode VARCHAR(50);
        DECLARE @GeneratedOrderNumber VARCHAR(50);
        DECLARE @OrderTotal DECIMAL(18,2);

        -- Table variable
        DECLARE @ItemData TABLE
        (
            ItemId UNIQUEIDENTIFIER,
            SizeId UNIQUEIDENTIFIER,
            ItemQuantity INT,
            ItemRate DECIMAL(18,2)
        );

        -- Get Shop Code
        SELECT @ShopCode = ShopCode
        FROM M_Shop
        WHERE ShopId = @ShopId;

        -- Insert Customer
        INSERT INTO M_Customer
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
        FROM @Customer;

        -- Generate Order Number
        SET @GeneratedOrderNumber = dbo.GenerateOrderNumber(@ShopId, @ShopCode);

        -- Store computed item data 
        INSERT INTO @ItemData
        SELECT
            i.ItemId,
            i.SizeId,
            i.ItemQuantity,
            dbo.fn_GetItemPrice(@ShopId, i.ItemId, i.SizeId, i.ItemQuantity)
        FROM @Items i;

        -- Insert Order Items
        INSERT INTO Map_Order_Items
        (
            MapId,
            OrderId,
            OrderNumber,
            ItemId,
            SizeId,
            ItemQty,
            Total,
            CreatedDate,
            DLM,
            IsActive
        )
        SELECT
            NEWID(),
            @OrderId,
            @GeneratedOrderNumber,
            ItemId,
            SizeId,
            ItemQuantity,
            ItemRate,
            GETDATE(),
            GETDATE(),
            1
        FROM @ItemData;

        -- Calculate Order Total
        SELECT @OrderTotal = ISNULL(SUM(ItemRate), 0)
        FROM @ItemData;

        -- Insert Order
        INSERT INTO Tbl_Shop_Order
        (
            OrderId,
            ShopId,
            OrderNumber,
            CustomerId,
            DeliveryAddress,
            Pincode,
            Phone,
            OrderTotal,
            CreatedDate,
            DLM,
            IsActive
        )
        SELECT
            @OrderId,
            @ShopId,
            @GeneratedOrderNumber,
            @CustomerId,
            CustomerAddress,
            Pincode,
            CustomerPhone,
            @OrderTotal,
            GETDATE(),
            GETDATE(),
            1
        FROM @Customer;

        SET @OrderNumber = @GeneratedOrderNumber;

        COMMIT TRANSACTION;
    END TRY

    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;