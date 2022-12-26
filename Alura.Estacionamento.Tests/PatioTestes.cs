using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class PatioTestes : IDisposable
    {
        private Patio estacionamento;
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsole;
        private Operador operador;

        public PatioTestes(ITestOutputHelper saidaConsole)
        {
            estacionamento = new Patio();
            veiculo = new Veiculo();
            SaidaConsole = saidaConsole;
            SaidaConsole.WriteLine("Construtor executado");
            operador = new Operador();
            operador.Nome = "José";
        }

        //arrange
        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            
            estacionamento.OperadorPatio = operador;
            veiculo.Proprietario = "André";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "rosa";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //act
            double faturamento = estacionamento.TotalFaturado();

            //assert
            Assert.Equal(2, faturamento);
        }
        [Theory]
        [InlineData("João", "ASD-1498", "preto", "Gol")]
        [InlineData("Fábio", "ASD-4789", "branco", "Uno")]
        [InlineData("Fernando", "ASD-1111", "amarelo", "Ferrari")]
        [InlineData("Paulo", "ASD-1123", "cinza", "Mercedes")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Modelo = modelo;
            veiculo.Cor = cor;
            veiculo.Placa = placa;
            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //act
            double faturamento = estacionamento.TotalFaturado();

            //assert
            Assert.Equal(2, faturamento);
        }



        [Theory]
        [InlineData("João", "ASD-1498", "preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNoIdDoTicket(string proprietario, string placa, string cor, string modelo)
        {
            //arrange
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Modelo = modelo;
            veiculo.Cor = cor;
            veiculo.Placa = placa;
            estacionamento.OperadorPatio = operador;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //assert 
            Assert.Contains("### Ticket Estacionamento Alura ###", consultado.Ticket);

        }

        [Fact] 
        public void AlterarDadosDoProprioVeiculo()
        {
            //arrange
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "André";
            veiculo.Cor = "rosa";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";
            estacionamento.OperadorPatio = operador;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "André";
            veiculoAlterado.Cor = "preto";
            veiculoAlterado.Modelo = "Fusca";
            veiculoAlterado.Placa = "asd-9999";
            

            //act
            Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);


        }

       

        public void Dispose()
        {
            SaidaConsole.WriteLine("Dispose invocado");
        }
    }
}
