using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SmartH2O_DLog
{

    public interface stringReceived
    {
        void onChanged();
    }

    class StorageHandler
    {
        private static DropboxClient dbx = null;


        public StorageHandler()
        {

            var task = Task.Run(Connect);

            task.Wait();
            if (dbx == null)
            {
                throw new Exception("Cannot connect to dropBox -> NO STORAGE!!");
            }



        }

        static async Task Connect()
        {
            dbx = new DropboxClient(Properties.Settings.Default.AcessToken);
            var full = await dbx.Users.GetCurrentAccountAsync();
            Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
        }

        public static async Task Upload()
        {
            string newInfo = File.ReadAllText("param-data.xml");
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(newInfo)))
            {
                try
                {
                    var updated = await dbx.Files.UploadAsync(
                        "/" + Properties.Settings.Default.FIleName,
                        WriteMode.Overwrite.Instance,
                        body: mem);
                    Console.WriteLine("\n Saved on DropBox " + DateTime.Now);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error curred in task UPLOAD: " + e.Message);
                }
            }
        }

        public void publishNewInformation()
        {
            try
            {
                var uploadInformations = Task.Run(Upload);
                uploadInformations.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ocurred when trying to publish: " + e.Message);
            }

        }

    }
}
