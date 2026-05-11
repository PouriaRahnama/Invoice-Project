namespace Invoice.Infrastructure.Common;

public static class SeedData
{
    public static IEnumerable<User> DefaultUsers =>
      new List<User>
      {
              new User()
              {
                  Username="",
                  PasswordHash="",
                  PasswordSalt=""
              }
      };
}

