using MediatR;
using MongoDB.Driver;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Queries.Users
{
    public class SendConfirmationCodeQuery : IRequest<bool>
    {
        public SendConfirmationCodeQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }

    public class SendConfirmationCodeQueryHandler : IRequestHandler<SendConfirmationCodeQuery, bool>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IEmailService _emailService;

        public SendConfirmationCodeQueryHandler(IMongoCollectionFactory collectionFactory, IEmailService emailService)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _emailService = emailService;
        }

    public async Task<bool> Handle(SendConfirmationCodeQuery request, CancellationToken cancellationToken)
        {
            var random = new Random();
            var code = random.Next(100000, 999999);
            var user = _users.Find(p => p.Email == request.Email).FirstOrDefault();
            if (user != null)
            {
                user.SecurityCode = code.ToString();
                await _users.ReplaceOneAsync(p => p.Email == request.Email, user, cancellationToken: cancellationToken);

                var subject = "Cod de Confirmare pentru Aplicația Car Marketplace";
                var body = $@"
                        <!DOCTYPE html>
                        <html lang='ro'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>Cod de Confirmare</title>
                        </head>
                        <body style='font-family: Arial, sans-serif; color: #333; margin: 0; padding: 0; background-color: #f4f4f4;'>
                            <div style='max-width: 600px; margin: auto; padding: 20px; background-color: #fff; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.1);'>
                                <h2 style='color: #007bff;'>Cod de Confirmare pentru Aplicația Car Marketplace</h2>
                                <p>Stimate utilizator,</p>
                                <p>Vă mulțumim că v-ați înregistrat în aplicația Car Marketplace!</p>
                                <p>Pentru a finaliza procesul de înregistrare, vă rugăm să introduceți codul de confirmare: </p>
                                <h3 style='font-size: 24px; color: #007bff; text-align: center;'>{code.ToString()}</h3>
                                <p>Dacă nu ați solicitat acest cod, vă rugăm să ignorați acest email.</p>
                                <p>Cu stimă, echipa Car Marketplace</p>
                            </div>
                        </body>
                        </html>";

                try
                {
                    await _emailService.SendEmailAsync(request.Email, subject, body);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    return false;
                }
            }
            return false;
        }
    }
}
