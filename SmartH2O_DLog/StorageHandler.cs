using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartH2O_DLog
{


    class StorageHandler
    {
        public StorageHandler()
        {
            var task = Task.Run(StorageHandler.Run);
            task.Wait();
            
        }

        static async Task Run()
        {
            using (var dbx = new DropboxClient(Properties.Settings.Default.AcessToken))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
        }

    }
}
