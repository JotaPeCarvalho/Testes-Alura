using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class VeiculoTeste : IDisposable
    {

        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsole;
        public VeiculoTeste(ITestOutputHelper saidaConsole)
        {
            veiculo = new Veiculo();
            SaidaConsole = saidaConsole;
            saidaConsole.WriteLine("Construtor executado");
        }

        [Fact]
        public void TestaVeiculoAcelerarComParametro10()
        {
            //Arrange
            // var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);

        }

        [Fact]
        public void TestaVeiculoFrearComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact(Skip = "Teste ainda não implementado - IGNORAR!")]
        public void ValidaNomeProprietarioDoVaiculo()
        {

        }

        [Fact]
        public void FichaDeInformacaoDoVeiculo()
        {
            //arrange
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "João";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "XAP-8899";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Variante";

            //act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do Veículo:", dados);

        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "ab";

            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
                );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            string placa = "ACDD3157";

            var mensagem = Assert.Throws<System.FormatException>(
                () => new Veiculo().Placa = placa
                ); ;

            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        public void Dispose()
        {
            SaidaConsole.WriteLine("Dispose invocado");
        }
    }
}
