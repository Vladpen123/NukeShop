
namespace NukeShop.WebUI.Infrastructure
{
    public static class ExtenionsMethods
    {
        public static IFormFile ByteArrayToIFormFile(this byte[] byteArray, string name, string filename)
        {
            FormFile formFile;
            using (MemoryStream ms = new(byteArray))
                formFile = new FormFile(ms, 0, ms.Length, name, filename);

            return formFile;
        }

        public static byte[] IFormFileToByteArray(this IFormFile formFile)
        {
            byte[]? byteArray = null;
            using (BinaryReader br = new(formFile.OpenReadStream()))
                byteArray = br.ReadBytes((int)formFile.Length);

            return byteArray;
        }

    }

}
