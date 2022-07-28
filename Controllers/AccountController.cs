using Microsoft.AspNetCore.Mvc;
using Sample_MachineTest.DatabaseConnection;
using Sample_MachineTest.DatabaseConnection.DatabaseClasses;
using Sample_MachineTest.Models;

namespace Sample_MachineTest.Controllers
{
    public class AccountController : Controller
    {
        SampleContext sampleContext;
        public AccountController(SampleContext sample)
        {
            this.sampleContext = sample;
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                return RedirectToAction("Employee", "Employee", new { @name = HttpContext.Session.GetString("name") });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                return RedirectToAction("Employee", "Employee", new { @name = HttpContext.Session.GetString("name") });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var register = new Register()
                {
                    UserName = registerModel?.Email?.Trim(),
                    Password = base64Encode(registerModel?.Password!),
                    FullName = registerModel?.FullName
                };
                sampleContext.Register.Add(register!);
                sampleContext.SaveChanges();

                ModelState.AddModelError("", "User registered Successfully Please Login.");
                return View(registerModel);
            }
            ModelState.AddModelError("", "Invalid Data");
            return View(registerModel);

        }

        [HttpPost]
        public IActionResult Login(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if (registerModel != null)
                {
                    string password = base64Encode(registerModel.Password!);
                    string? email = registerModel?.Email?.Trim();
                    
                    var isValidUser = sampleContext.Register.Where(x => x.UserName == email && x.Password == password).FirstOrDefault();

                    if (isValidUser != null)
                    {
                        HttpContext.Session.SetString("email", isValidUser.UserName);
                        HttpContext.Session.SetString("name", isValidUser.FullName);

                        return RedirectToAction("Employee","Employee",new { @name = isValidUser.FullName });
                    }
                    else {
                        ModelState.AddModelError("", "No Record Found");
                    }
                }
            }
            return View(registerModel);

        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Login");
        }

        public string base64Encode(string sData) // Encode    
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public string base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
    }
}
