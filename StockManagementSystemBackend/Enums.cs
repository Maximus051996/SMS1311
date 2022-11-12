using System.ComponentModel;

namespace StockManagementSystemBackend
{
    public enum Enums
    {
        [Description("Operation Succeeded")]
        Success=1,
        [Description("Operation Failed")]
        Failure =2,
        [Description("Record Updated Successfully")]
        Update =3,
        [Description("Record Disabled Successfully")]
        Delete =4,
        [Description("Records Fetched Successfully")]
        Fetch =5,
        [Description("Record Added Successfully")]
        Insert =6,
        [Description("Record Enabled Successfully")]
        Active=7,
        [Description("Incorrect UserName Or Password")]
        LoginFailed=8,
        [Description("Please Fill All the Fields")]
        ValidationError=9,
        [Description("Record Already Exist")]
        Dublicate,
        [Description("Unknown Error Came, Please Try Again After some time")]
        Unknown,
        [Description("File Is Empty")]
        FileEmpty,
        [Description("File Format Is Not Correct")]
        FileFormat,
        [Description("User not have Permission")]
        UserInvalid

    }

    public static class Common
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attr=field?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr?.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
        public static string getRandColor()
        {
            Random rnd = new Random();
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return "#" + hexOutput;
        }
    }
}
