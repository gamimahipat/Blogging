namespace BloggingAPI.v1
{
    public interface IUsersRepository
    {
        Task<ApiResponse> GetUsers();
        Task<ApiResponse> GetUserById(int id);
        Task<ApiResponse> PostUsers(UserSaveViewModel user);
        Task<ApiResponse> AuthenticateUser(LoginViewModel login);
        //Task<ApiResponse> UpdateUserAsync(Users user);
        //Task<ApiResponse> DeleteUserAsync(int id);
        //Task<ApiResponse> UserExistsAsync(int id);
    }
}
