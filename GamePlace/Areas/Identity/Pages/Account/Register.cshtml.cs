using GamePlace.Data;
using GamePlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GamePlace.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {

        //     private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        // private readonly IEmailSender _emailSender;

        /// <summary>
        /// atributo que referencia a Base de Dados do projeto
        /// </summary>
        private readonly GamePlaceDb _db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            //    SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            //    IEmailSender emailSender
            GamePlaceDb db
           )
        {
            _userManager = userManager;
            //   _signInManager = signInManager;
            _logger = logger;
            //  _emailSender = emailSender;
            _db = db;
        }

        /// <summary>
        /// objeto que irá transportar os dados entre o formulário e o 'código'
        /// i.e., irá transportar os dados entre o Browser e o Servidor
        /// </summary>
        [BindProperty] // adiciona 'memória' ao HTTP
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        //  public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ter, no mínimo {2} carateres e, no máximo, {1} carateres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "A password e a sua confirmação não coincidem.")]
            public string ConfirmPassword { get; set; }

            /// <summary>
            /// atributo para recolher os dados do Utilizador que está a criar a conta
            /// </summary>
            public UtilizadorRegistado Utilizador { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //  ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        /// <summary>
        /// este método é acedido se a página devolver o controlo em HTTP POST
        /// é ele que cria o novo utilizador
        /// </summary>
        /// <param name="returnUrl">'link' para reposicionar o utilizador para a página que desejava ir</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            // se 'returnUrl' for nulo, altera o seu valor
            returnUrl ??= Url.Content("~/");

            //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // se entrei aqui, é pq os dados recolhidos são válidos

                // criar um objecto do tipo 'user'
                // com os dados da pessoa q se registou
                var user = new IdentityUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    /*LockoutEnd = new DateTime(DateTime.Now.Year + 10,
                                              DateTime.Now.Month,
                                              DateTime.Now.Day)*/
                };

                // vou tentar criar o utilizador
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // se aqui cheguei, é pq foi criado com sucesso o novo utilizador
                    // primeiro q tudo, vou adicionar esse novo utilizador ao Role 'Utilizador'
                    // depois, é preciso guardar os dados do novo Utilizador
                    //   -- é preciso obter os dados do Utilizador
                    //   -- guardá-los na base de dados

                    //Verifica se o email colocado é do gestor e se for coloca essa conta como gestor, caso contrário colocado a conta como cliente
                    if (Input.Email == "admin@ipt.pt")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Utilizador");
                    }
                    try
                    {
                        // adicionar ao Role
                        await _userManager.AddToRoleAsync(user, "Utilizador");

                        // recuperar os dados do Utilizador
                        // atribuir ao Utilizador o email que será usado na autenticação
                        Input.Utilizador.Email = Input.Email;
                        // atribuir ao Utilizador o ID do user q acabou de se criar
                        Input.Utilizador.UserNameId = user.Id;


                        // guardar os dados na BD
                        await _db.AddAsync(Input.Utilizador);

                        // consolidar a operação de guardar
                        await _db.SaveChangesAsync();

                        // já não há nada a fazer,
                        // redirecionar para a página de confirmação de criação de conta
                        return RedirectToAction(nameof(Index), "Jogos");
                    }
                    catch (Exception)
                    {
                        // houve um erro na criação de um Utilizador
                        // Além da mensagem de erro,
                        ModelState.AddModelError("", "Houve um erro com a criação do utilizador");
                        //  deverá ser apagada o User q foi previamente criado
                        await _userManager.DeleteAsync(user);
                        // devolver os dados à página
                        return Page();
                    }
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount) {
                    //   return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else {
                    //   await _signInManager.SignInAsync(user, isPersistent: false);
                    //   return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
