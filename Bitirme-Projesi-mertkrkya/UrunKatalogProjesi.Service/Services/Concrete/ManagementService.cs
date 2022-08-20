using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrunKatalogProjesi.Data.Configurations;
using UrunKatalogProjesi.Data.Entities;
using UrunKatalogProjesi.Service.Services.Abstract;
using UrunKatalogProjesi.Service.Validations;

namespace UrunKatalogProjesi.Service.Services.Concrete
{
    public class ManagementService : IManagementService
    {
        private readonly IHostingEnvironment _env;
        private readonly SystemOptionConfig userOptionsConfig;
        public ManagementService(IHostingEnvironment env, IOptions<SystemOptionConfig> options)
        {
            _env = env;
            userOptionsConfig = options.Value;
        }
        public async Task<ResponseEntity> UploadPhoto([FromForm] IFormFile image)
        {
            int maxFileSize = Convert.ToInt32(userOptionsConfig.MaxFileKbSize); //Değiştir.
            var validateResult = FileValidation.ImageValidation(image, maxFileSize);
            if (!string.IsNullOrWhiteSpace(validateResult))
                return new ResponseEntity(validateResult);
            try
            {
                var docName = Path.GetFileName(image.FileName);
                var fileName = userOptionsConfig.ImportPhotoDirectory + "/" + DateTime.Now.ToString("Mddyyyyhhmmss") + docName;
                FileStream fs = new FileStream(fileName, FileMode.Create);
                image.CopyTo(fs);
                fs.Close();
                return new ResponseEntity(new FileResponse(fileName));
            }
            catch (Exception e)
            {

                throw new Exception("File Upload Error. Message: " + e.Message);
            }
        }
        public class FileResponse
        {
            public string FileLink { get; set; }
            public FileResponse(string fileLink)
            {
                FileLink = fileLink;
            }
        }
    }
}
