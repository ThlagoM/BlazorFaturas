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

        public async Task<Dictionary<string, bool>> ImportarFaturas()
        {
            var resultParse = new Dictionary<string, bool>();

            var arquivos = Directory.GetFiles(_xmlInputPath, "*.xml");

            foreach (var arquivo in arquivos)
            {
                try
                {
                    var xmlDoc = XDocument.Load(arquivo);
                    var nomeFornecedor = xmlDoc.Root.Element("nomeFornecedor")?.Value;
                    var numeroFatura = xmlDoc.Root.Element("numeroFatura")?.Value;
                    var dataDocumento = DateTime.Parse(xmlDoc.Root.Element("dataDocumento")?.Value);
                    var valorTotal = decimal.Parse(xmlDoc.Root.Element("valorTotal")?.Value);

                    var pdfFileName = Path.ChangeExtension(arquivo, ".pdf");
                    byte[] pdfFileBytes = null;

                    if (File.Exists(pdfFileName))
                    {
                        pdfFileBytes = await File.ReadAllBytesAsync(pdfFileName);

                        // Move o arquivo PDF para a pasta de destino
                        var pdfDestinationPath = Path.Combine(_xmlMovedPath, Path.GetFileName(pdfFileName));
                        File.Move(pdfFileName, pdfDestinationPath);
                    }

                    var fatura = new Fatura
                    {
                        NomeFornecedor = nomeFornecedor,
                        NumeroFatura = numeroFatura,
                        DataDocumento = dataDocumento,
                        ValorTotal = valorTotal,
                        Ativo = true,
                        OriginalFileName = Path.GetFileName(pdfFileName),
                        OriginalFileNameBytes = pdfFileBytes
                    };

                    _context.Faturas.Add(fatura);
                    await _context.SaveChangesAsync();

                    // Move o arquivo XML para a pasta de destino
                    var xmlDestinationPath = Path.Combine(_xmlMovedPath, Path.GetFileName(arquivo));
                    File.Move(arquivo, xmlDestinationPath);

                    resultParse.Add(Path.GetFileName(arquivo), true);
                }
                catch (Exception ex)
                {
                    resultParse.Add(Path.GetFileName(arquivo), false);
                }
            }

            return resultParse;
        }
    }
}