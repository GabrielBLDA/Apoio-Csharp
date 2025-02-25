﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Quinto_Projeto
{
    internal class Program
    {

        static string delimitadorInicio;
        static string delimitadorFim;
        static string tagNome;
        static string tagDataNascimento;
        static string tagNomeDaRua;
        static string tagNumeroDaCasa;
        public struct DadosCadastraisStruct
        {
            public string Nome;
            public DateTime DataDeNascimento;
            public string NomeDaRua;
            public UInt32 NumeroDaCasa;
        }

        public enum Resultado_e
        {
            sucesso = 0,
            sair = 1,
            Excecao = 2
        }

        public static void MostraMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }

        public static Resultado_e PegaString(ref string minhaString, string mensagem)
        {
            Resultado_e retorno;
            Console.WriteLine(mensagem);
            string temp = Console.ReadLine();
            if (temp == "s" || temp == "S")
            {
                retorno = Resultado_e.sair;
            }
            else
            {
                minhaString = temp;
                retorno = Resultado_e.sucesso;
            }
            Console.Clear();
            return retorno;
        }

        public static Resultado_e PegaData(ref DateTime data, string mensagem)
        {
            Resultado_e retorno;
            do
            {
                try
                {
                    Console.WriteLine(mensagem);
                    string temp = Console.ReadLine();
                    if (temp == "s" || temp == "S")
                    {
                        retorno = Resultado_e.sair;
                    }
                    else
                    {
                        data = Convert.ToDateTime(temp);
                        retorno = Resultado_e.sucesso;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Execessão: {e.Message}");
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    retorno = Resultado_e.Excecao;
                }


            } while (retorno == Resultado_e.Excecao);
            Console.Clear();
            return retorno;
        }

        public static Resultado_e PegaUint32(ref UInt32 numeroUint32, string mensagem)
        {
            Resultado_e retorno;
            do
            {
                try
                {
                    Console.WriteLine(mensagem);
                    string temp = Console.ReadLine();
                    if (temp == "s" || temp == "S")
                    {
                        retorno = Resultado_e.sair;
                    }
                    else
                    {
                        numeroUint32 = Convert.ToUInt32(temp);
                        retorno = Resultado_e.sucesso;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Execessão: {e.Message}");
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    retorno = Resultado_e.Excecao;
                }


            } while (retorno == Resultado_e.Excecao);
            Console.Clear();
            return retorno;
        }

        public static Resultado_e CadastrarUsuario(ref List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            DadosCadastraisStruct cadastroUsuario;
            cadastroUsuario.Nome = "";
            cadastroUsuario.DataDeNascimento = new DateTime();
            cadastroUsuario.NomeDaRua = "";
            cadastroUsuario.NumeroDaCasa = 0;
            if (PegaString(ref cadastroUsuario.Nome, "Digite o nome completo ou Digite S para Sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if (PegaData(ref cadastroUsuario.DataDeNascimento, "Digite a data de nascimento no formato DD/MM/AAAA ou digite S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if (PegaString(ref cadastroUsuario.NomeDaRua, "Digite o Nome da rua ou digite S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if (PegaUint32(ref cadastroUsuario.NumeroDaCasa, "Digite o Numero da casa ou digite S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            ListaDeUsuarios.Add(cadastroUsuario);
            return Resultado_e.sucesso;
        }

        public static void GravaDados(string caminho, List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            try
            {
                string conteudoArquivo = "";
                foreach(DadosCadastraisStruct cadastro in ListaDeUsuarios)
                {
                    conteudoArquivo += delimitadorInicio + "\r\n";
                    conteudoArquivo += tagNome + cadastro.Nome + "\r\n";
                    conteudoArquivo += tagDataNascimento + cadastro.DataDeNascimento.ToString("dd/MM/yyyy") + "\r\n";
                    conteudoArquivo += tagNomeDaRua + cadastro.NomeDaRua + "\r\n";
                    conteudoArquivo += tagNumeroDaCasa + cadastro.NumeroDaCasa + "\r\n";
                    conteudoArquivo += delimitadorFim + "\r\n";
                }
                File.WriteAllText(caminho, conteudoArquivo);
            }
            
            catch (Exception e)
            {
                Console.WriteLine($"excecao: {e.Message}");
            }
        }

        public static void CarregaDados(string caminho, ref List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            try
            {
                if(File.Exists(caminho))
                {
                    string[] conteudoArquivo = File.ReadAllLines(caminho);
                    DadosCadastraisStruct dadosCadastrais;
                    dadosCadastrais.Nome = "";
                    dadosCadastrais.DataDeNascimento = new DateTime();
                    dadosCadastrais.NomeDaRua = "";
                    dadosCadastrais.NumeroDaCasa = 0;

                    foreach (string linha in conteudoArquivo)
                    {
                        if (linha.Contains(delimitadorInicio))
                            continue;
                        if (linha.Contains(delimitadorFim))
                            ListaDeUsuarios.Add(dadosCadastrais);
                        if (linha.Contains(tagNome))
                            dadosCadastrais.Nome = linha.Replace(tagNome, "");
                        if (linha.Contains(tagDataNascimento))
                            dadosCadastrais.DataDeNascimento = Convert.ToDateTime(linha.Replace(tagDataNascimento,""));
                        if (linha.Contains(tagNomeDaRua))
                            dadosCadastrais.NomeDaRua = linha.Replace(tagNomeDaRua,"");
                        if (linha.Contains(tagNumeroDaCasa))
                            dadosCadastrais.NumeroDaCasa = Convert.ToUInt32(linha.Replace(tagNumeroDaCasa, ""));
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"excecao: {e.Message}");
            }
        }

        static void Main(string[] args)
        {
            List<DadosCadastraisStruct> ListaDeUsuarios = new List<DadosCadastraisStruct>();
            string opcao = "";
            delimitadorInicio = "##### INICIO #####";
            delimitadorFim = "##### INICIO #####";
            tagNome = "NOME: ";
            tagDataNascimento = "DATA_DE_NASCIMENTO: ";
            tagNomeDaRua = "NOME_DA_RUA: ";
            tagNumeroDaCasa = $"NUMERO_DA_CASA: ";
            string caminhoArquivo = @"C:\Users\klebe\Desktop\teste\Cadastro.txt";

            CarregaDados(caminhoArquivo, ref ListaDeUsuarios);

            do
            {
                Console.WriteLine("Digite C para cadastrar um novo usuario ou S para sair");
                opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();
                if (opcao == "c")
                {
                    if(CadastrarUsuario(ref ListaDeUsuarios) == Resultado_e.sucesso);
                    {
                        GravaDados(caminhoArquivo, ListaDeUsuarios);
                    }
                }
                else if (opcao == "s")
                {
                    MostraMensagem("Encerrando o programa");
                }
                else
                {
                    MostraMensagem("Opção desconhecida");

                }
            } while (opcao != "s");

        }
    }
}