## Database ERD

```mermaid
erDiagram

    M_User ||--|| Tbl_ActiveUser : logs_in

    M_ShopType ||--o{ M_Shop : has
    M_ShopStatus ||--o{ M_Shop : status

    M_Shop ||--o{ Tbl_Shop_Order : receives
    M_Customer ||--o{ Tbl_Shop_Order : places

    Tbl_Shop_Order ||--o{ Map_Order_Items : contains
    Tbl_Shop_Order ||--|| Map_Order_Bill : billed

    M_Item ||--o{ Map_Order_Items : ordered

    M_Shop ||--o{ Map_Shop_Item : sells
    M_Item ||--o{ Map_Shop_Item : listed_in
    M_ItemSize ||--o{ Map_Shop_Item : sized

    M_ItemGroup ||--o{ M_Item : groups
    M_ItemCategory ||--o{ M_Item : categorizes
    M_ItemSize ||--o{ M_Item : defines

    M_User ||--o{ Tbl_OtpStore : verifies

    M_User {
        userId uuid
        UserName string
        UserPhone string
        UserEmail string
        Password string
        Role string
    }

 M_ShopType {
        ShopTypeId uuid
        TypeName string
        IsActive bool
    }

    M_ShopStatus {
        StatusId uuid
        StatusName string
        Prefix string
        Colour string
        Role string
        IsActive bool
    }

    Tbl_Shop_Order {
        OrderId uuid
        OrderNumber string
        ShopId uuid
        CustomerId uuid
        DeliveryAddress string
        Pincode string
        Phone string
        IsActive bool
    }

    Map_Shop_Item {
        MapId uuid
        ShopId uuid
        ItemId uuid
        SizeId uuid
        Qty int
        Price decimal
        Discount decimal
        StockQty int
        IsActive bool
    }

    M_Shop {
        ShopId uuid
        ShopName string
        ShopAddress string
        Pincode string
        ShopCode string
        ShopPhone string
        ShopLogo string
        ShopImage string
        GstNo string
        Latitude string
        Longitude string
        StatusId uuid
        ShopTypeId uuid
        OpeningTime datetime
        ClosingTime datetime
        NearByLocation string
        IsActive bool
    }

    Map_Order_Items {
        MapId uuid
        OrderId uuid
        ItemId uuid
        ItemQty int
        ItemRate decimal
        IsActive bool
    }

    Map_Order_Bill {
        MapId uuid
        ShopId uuid
        InvoiceNumber string
        OrderId uuid
        DeliveryCharge decimal
        OrderTotal decimal
        NetTotal decimal
        IsActive bool
    }

    M_ItemSize {
        SizeId uuid
        SizeName string
        IsActive bool
    }

    M_ItemGroup {
        GroupId uuid
        GroupName string
        IsActive bool
    }

    M_ItemCategory {
        CategoryId uuid
        CategoryName string
        IsActive bool
    }

    M_Item {
        ItemId uuid
        ItemName string
        ItemDescription string
        ItemGroupId uuid
        ItemCategoryId uuid
        ItemSizeId uuid
        ItemCode string
        ItemImage string
        IsActive bool
    }

    M_Customer {
        CustomerId uuid
        CustomerName string
        CustomerPhone string
        CustomerEmail string
        CustomerAddress string
        Pincode string
        Password string
        IsActive bool
    }

     Tbl_ActiveUser {
        MapId uuid
        UserId uuid
        AccessToken string
        RefreshToken string
        LoginDateTime datetime
    }

    Tbl_OtpStore {
        TrackId uuid
        Email string
        Otp string
        createTime datetime
        ExpiryTime datetime
        IsUsed bool
    }
```
