namespace Invoice.Application.GridifyMappers
{
    public class UserGridifyMapper : GridifyMapper<GetAllUserAccountsDto>
    {
        public UserGridifyMapper()
        {
            AddMap("Phone", p => p.Phone);
            AddMap("UserId", p => p.UserId);
            AddMap("Username", p => p.Username);
        }
    }
}
