# CityShop API

CityShop is an ASP.NET Core REST API for managing shops, items, customers, orders, users, and OTP authentication. It is built with Entity Framework Core and SQL Server, and exposes controllers for e-commerce operations around shop listings, catalog management, order processing, and user authentication.

## Key Features

- User registration and JWT-based login
- Shop management with shop types and shop status
- Item catalog management with categories, groups, and sizes
- Shop item mapping for pricing, stock, and discounts
- Order management with bills and order item mapping
- Customer management
- OTP verification support
- Centralized exception handling
- Swagger API documentation for development

## Architecture

The project follows a layered architecture:

- `Controllers/` - API controllers for each domain entity
- `Services/` - business logic implementations
- `Repositories/` - data access abstractions and implementations
- `Interfaces/` - service and repository contracts
- `Model/` - EF Core entity models and `ApplicationDBContext`
- `DTO/` - request/response transfer objects
- `Helpers/` - utility classes such as encryption, JWT token generation, and response helpers
- `Middleware/` - custom middleware for consistent error handling

## Database

The API is designed for SQL Server and uses Entity Framework Core for data access. The `ApplicationDBContext` defines DbSet entities including:

- `Users`, `ActiveUsers`, `OtpStores`
- `Customers`
- `Shops`, `ShopTypes`, `ShopStatuses`
- `Items`, `ItemCategories`, `ItemGroups`, `ItemSizes`
- `ShopItems`, `ShopOrders`, `OrderItems`, `OrderBills`, `Reviews`

## Getting Started

### Setup

1. Clone the repository.
2. Open the solution `cityshop-api.sln`.
3. Configure the SQL Server connection string in `appsettings.json` or `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CityShopDb;Trusted_Connection=True;"
}
```

4. Configure JWT settings in `appsettings.json`:

```json
"Jwt": {
  "Key": "YOUR_SECRET_KEY",
  "Issuer": "your-app",
  "Audience": "your-app-audience"
}
```

5. Apply database migrations or create the schema manually using the SQL files in `DB/tables.sql`.

## Authentication

The API uses JWT Bearer authentication. Public endpoints such as `register` and `login` are allowed anonymously, while other endpoints require a valid JWT token.

## Important Controllers

- `UserController` - register users and login
- `ShopController` - manage shops
- `ItemController` - manage items
- `ShopItemMapController` - manage shop-specific item listings and pricing
- `OrderController` - create and manage shop orders
- `CustomerController` - manage customer records
- `ShopTypeController`, `ShopStatusController`, `ItemCategoryController`, `ItemGroupController`, `ItemSizeController` - manage related lookup data

## Tools and Dependencies

- ASP.NET Core 6
- Entity Framework Core 6
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.AspNetCore.Authentication.JwtBearer
- Swashbuckle.AspNetCore for Swagger
- MailKit for email handling

## Notes

- CORS is configured to allow all origins, methods, and headers.
- Custom exception middleware is used to standardize API error responses.
- Routes are configured to use lowercase URLs.

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
