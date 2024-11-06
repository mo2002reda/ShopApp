namespace Shop_web.Helper
{
    public static class DocumentationSettings
    {
        public static string Upload(IFormFile file ,string Filename)
        {
            //1)Get Folder Path which Uploaded to
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Products");
            //2)Create File Name
            string FileName = $"{Guid.NewGuid()}_{file.FileName}";
            //3)Get File Path[Folder Path+ FileName]
            string FilePath = Path.Combine(FolderPath, FileName);
            //4)Save File As Stream [Like open Data Base]:FileStream is a database For Files
            using var Fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);
            return FileName;
        }
        public static void DeleteFile(string FileName, string FolderName)
        {
            //1)Get Fie Path 
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", FolderName, FileName);
           

            //2)Check If File Exist Or Not
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }

        }
    }
}
