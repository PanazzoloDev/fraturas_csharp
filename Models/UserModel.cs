namespace fraturas_csharp.Models
{
    public class UserViewModel : UserNewModel
    {
        public int Id { get; set; }
    }
    public class UserNewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateModel : UserNewModel
    {
        
    }
}