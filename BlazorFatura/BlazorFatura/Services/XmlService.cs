using BlazorFatura.Configuration;
using BlazorFatura.Data;
using BlazorFatura.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorFatura.Services
{
    public class XmlService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _xmlInputPath;
        private readonly string _xmlMovedPath;

        public XmlService(ApplicationDbContext context, IOptions<AppSettings> settings)
        {
            _context = context;
            _xmlInputPath = settings.Value.XmlInputPath;
            _xmlMovedPath = settings.Value.XmlOutputFolder;
        }

        public async Task<bool> ImportarFaturas()
        {
            var arquivos = Directory.GetFiles(_xmlInputPath, "*.xml");
            var arquivosComErro = new List<string>();

            foreach (var arquivo in arquivos)
            {
                try
                {
                    var xmlDoc = XDocument.Load(arquivo);
                    var fatura = new Fatura
                    {
                        NomeFornecedor = xmlDoc.Root.Element("nomeFornecedor")?.Value,
                        NumeroFatura = xmlDoc.Root.Element("numeroFatura")?.Value,
                        DataDocumento = DateTime.Parse(xmlDoc.Root.Element("dataDocumento")?.Value),
                        ValorTotal = decimal.Parse(xmlDoc.Root.Element("valorTotal")?.Value),
                        Ativo = true
                    };

                    _context.Faturas.Add(fatura);
                    await _context.SaveChangesAsync();

                    // Move o arquivo XML para a pasta 'Inseridos'
                    var fileName = Path.GetFileName(arquivo);
                    var destinationPath = Path.Combine(_xmlMovedPath, fileName);
                    File.Move(arquivo, destinationPath);
                }
                catch (Exception ex)
                {
                    arquivosComErro.Add(Path.GetFileName(arquivo));
                    // Pode logar ou tratar de outra forma o erro individual
                }
            }

            if (arquivosComErro.Any())
            {
                // Se houver arquivos com erro, não movemos nenhum arquivo
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}