using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace ExemploCicloDeVidaServicos
{
    class Program
    {
        public static void Main(string[] args)
        {


            //AddScoped:      
            //Uma única instância é criada para cada solicitação HTTP.
            //Essa instância é compartilhada e reutilizada durante todo o processamento da mesma solicitação.
            //Quando uma nova solicitação HTTP é feita, uma nova instância é criada.

            //AddSingleton:     
            //Uma única instância é criada quando o aplicativo é iniciado.
            //Essa instância é compartilhada entre todas as solicitações HTTP e durante toda a execução do aplicativo.
            //A mesma instância é usada em todas as partes do aplicativo.

            //AddTransient:    
            //Uma nova instância é criada toda vez que o serviço é solicitado, independentemente da solicitação HTTP.
            //Cada solicitação HTTP obtém sua própria instância isolada do serviço.
            //Não há compartilhamento de instâncias entre solicitações.

            // Configurar os serviços

            //Descomente a instância do serviço que deseje testar e verifique seu respectivo funcionamento
            //executando o projeto
         
            var serviceProvider = new ServiceCollection()
                .AddScoped<IServico, ServicoScoped>()
                //.AddSingleton<IServico, ServicoSingleton>()
                //.AddTransient<IServico, ServicoTransient>()
                .BuildServiceProvider();

            // Usar os serviços
            Console.WriteLine("Exemplo de Ciclo de Vida de Serviços:");

            Console.WriteLine("");
            Console.WriteLine("Requisição 1:");
            using (var scope = serviceProvider.CreateScope())
            {
                var servico1 = scope.ServiceProvider.GetService<IServico>();
                var servico2 = scope.ServiceProvider.GetService<IServico>();
                var servico3 = scope.ServiceProvider.GetService<IServico>();

                Console.WriteLine($"Serviço 1: {servico1.GetId()}");
                Console.WriteLine($"Serviço 2: {servico2.GetId()}");
                Console.WriteLine($"Serviço 3: {servico3.GetId()}");
            }

            Console.WriteLine("");
            Console.WriteLine("Requisição 2:");
            using (var scope = serviceProvider.CreateScope())
            {

                var servico1 = scope.ServiceProvider.GetService<IServico>();
                var servico2 = scope.ServiceProvider.GetService<IServico>();
                var servico3 = scope.ServiceProvider.GetService<IServico>();

                Console.WriteLine($"Serviço 1: {servico1.GetId()}");
                Console.WriteLine($"Serviço 2: {servico2.GetId()}");
                Console.WriteLine($"Serviço 3: {servico3.GetId()}");
            }

            Console.ReadLine();
        }
    }

    public interface IServico
    {
        Guid GetId();
    }

    public class ServicoScoped : IServico
    {
        private Guid _id;

        public ServicoScoped()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId() => _id;
    }

    public class ServicoSingleton : IServico
    {
        private Guid _id;

        public ServicoSingleton()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId() => _id;
    }

    public class ServicoTransient : IServico
    {
        private Guid _id;

        public ServicoTransient()
        {
            _id = Guid.NewGuid();
        }

        public Guid GetId() => _id;
    }
}
