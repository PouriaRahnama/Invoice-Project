namespace Invoice.Infrastructure.Common;

public static class SeedData
{
    public static IEnumerable<User> DefaultUsers =>
      new List<User>
      {
            new User
            {
                Id = Guid.Parse("6712adb7-a20d-43e9-8b29-357271f3bd65"),
                Username = "adminUser",
                PasswordHash = "3c830e2af2e5db02e2f467634499d3c807e7b0c1b09c247a478c893703999b0e",  //123456
                PasswordSalt = "6081de2f-df32-4e79-a844-772054b8fb32",
                Phone = "09121234567",

            },
            new User
            {
                Id = Guid.Parse("92aa3814-ee96-4593-bdd3-cd613268137a"),
                Username = "adminUser2",
                PasswordHash = "3c830e2af2e5db02e2f467634499d3c807e7b0c1b09c247a478c893703999b0e", //123456
                PasswordSalt = "6081de2f-df32-4e79-a844-772054b8fb32",
                Phone = "09139876543",
            }
      };

    public static IEnumerable<Customer> DefaultCustomers =>
          new List<Customer>
          {
                    new Customer
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse("6712adb7-a20d-43e9-8b29-357271f3bd65") ,
                        FullName = "شرکت الفبا",
                        Phone = "02112345678",
                        Address = "تهران، خیابان اول، پلاک ۱",

                    },
                    new Customer
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse("92aa3814-ee96-4593-bdd3-cd613268137a"),
                        FullName = "1شرکت الفبا",
                        Phone = "02112345671",
                        Address = "تهران، خیابان اول، پلاک 2",
                    }
          };
    public static IEnumerable<Product> DefaultProducts =>
      new List<Product>
      {
                    new Product
                     {
                            Id = Guid.NewGuid(),
                            Code = "PRD-20260514-A1B2C3",
                            Name = "لپ تاپ مدل X1",
                            Price = 55000000,
                            Quantity = 15,

                     },
                     new Product
                     {
                            Id = Guid.NewGuid(),
                            Code = "PRD-20260514-D4E5F6",
                            Name = "کیبورد مکانیکی RGB",
                            Price = 2500000,
                            Quantity = 50,
                     }
      };
}

