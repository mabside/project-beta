namespace Byhands.API
{
    public class UserConstant
    {
        public static List<UserModel> UserModels = new List<UserModel>()
        {
            new UserModel() { 
                Id = "5cab63c0-a54e-41e9-a3cc-00effeffd54c", 
                Email = "tester1@gmail.com", 
                Password = "tester_1test", 
                Firstname = "Tester1", 
                Lastname = "Wilbur1"
            },
            new UserModel() { 
                Id = "559d6983-f64d-4bde-bd15-d4401bcf94dd", 
                Email = "tester2@gmail.com", 
                Password = "tester_2test", 
                Firstname = "Tester2", 
                Lastname = "Wilbur2"
            },
        };
    }
}
