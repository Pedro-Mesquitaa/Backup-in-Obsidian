using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO.Compression;
using System.Threading.Tasks;

class DropboxUpload
{
    private static string dropboxToken = "sl.u.AFTyRQnzuC2a5JRgDsa-MPwYVfAKiblm7jIvi96fLqqUyLzG0gKOe7VK0Q6GQkn7Co4afBy4kASE8W0VXIFfSFaracie3GzKtVcbnG_dLhUCseSqfrpCbY9IFKVgpQ96Da9nvVkq24_mjOBfgoFej_aabrDgJRXPwHNZJ6_r_lL8jPPbA0zjzZ3jGt-N218e5K9vOPNz5Wtzwc1qTDhweD1vTder_XI7LlQeBaGJHA0-Ec9t4dKgtscva1iY8QJsy36xKRSoBXouphGdYf5Q1W33ZTobqUP1W2kLidh3bQOC_rhbr4A9_KwR9ykcM0s9Rwh_iVCS0kHrg9vhh6G0XsMekE3dva9kQpdhWYr7SV2r0itCgj5RN1MmN0TMV4VVWw7rPpas4LYzntscpbJ9JxC35UjMOfZUGR0gzHL0GJptdsCR7oOhexDL1hh3IuvXCFpTgShPtAWYODa4I559JYYT9SGT9mj_6OlCBzNa8eDRGx6Vld5onfaYVzdX3avRs5jomS3hBCauAK1K-okI7sMIvovUbOC9yejCqqdGu0RIvnHjztDKWGN1jpRBGeHV-O_C7KUrS6djG6RHpcHNsWGozYBw0hM4aHTQSyxAlSfAO3XYrzYNZwunEpJKomkHJzKa7b3krcyPzz0Av4Tvxq3VT__ud3soJQ7e8d3OqxaTsLx-lnNSiiW3_xtQrHivZiu2J0b2yRxHy6acGMDPVqFDLHVC7QGmzLcV78tYzPkW4oqJKV-ygqbNOChXM2LevyPa3jPOkr3tOPacTGDLSSXTVUZKfQ7nyi_kA79zntN3WAAH4maZCK6gC2HuVB-ND44zbemxba4lVDFC_i6SPjYfmdJR2OXGqBauXBmDYvYsZuJng7_ak5W-k6WAwvlSub1Syz6J6-Hy4LfFo3pQ4928l2ziy6sIocShqOPMObw7INo6-YX6VcfQA5uq3v5fMl-MrCaY64_gUPFDkOwjEDzhyhsuDFb-VdK-7tiFGOvxJxFxUZP-ef3KmJbRcZ25UUgZf_sn_CTHuk8bp1vFmzox8KWCPN0NkICX11933lJrpTJ_xtkkaUzfAbex-kGLMWcYl5_2GIerjqqngkWTtw-Ux0DBngPEvwlGRDZkJN7ovNtCJVXnkpYLWl7UwFGZpQYbujQuLJYi3ihGypbO77oiwnT5QpV4SWZU7RpRrPZAt7rtD547h1ndZ8qoTYE4ebkJUx4l384gdHapHZF8-cGY3bP_8sMxg1ZO1I8CKsZP4XgedpxXAourMCrsVPbV79HykLIlYSRmRHQQ2Vq2rPldYf6TlGp7PCgxes6k6TsQeXf-jY7PVyxKY-1FEXeGQrb0jJE_7Q4FAgY0gRIqmwOGgLte5GS9STH0gT7UzrlAcVGNvbCNRZwSq7cMpj-IVartP75KgWOrOuq3mvvh7hEAW7mekl5Vp2H2wksDqZfdq4KkFq0i7x54VdHjsCo7KZk";  // Substitua com seu token de acesso

    // Método para compactar uma pasta
    public static void CompactarPasta(string pastaOrigem, string caminhoZipDestino)
    {
        ZipFile.CreateFromDirectory(pastaOrigem, caminhoZipDestino);
    }

    // Método para enviar o arquivo ZIP para o Dropbox
    public static async Task EnviarParaDropbox(string caminhoZip)
    {
        using (var dbx = new DropboxClient(dropboxToken))
        {
            using (var fileStream = File.Open(caminhoZip, FileMode.Open))
            {
                // Cria um nome para o arquivo no Dropbox (exemplo: "/arquivo.zip")
                var nomeArquivoDropbox = "/" + Path.GetFileName(caminhoZip);

                // Realiza o upload para o Dropbox
                var response = await dbx.Files.UploadAsync(
                    nomeArquivoDropbox, WriteMode.Overwrite.Instance, body: fileStream);

                Console.WriteLine("Arquivo enviado com sucesso: " + response.PathLower);
            }
        }
    }

    // Método principal para compactar e enviar
    public static async Task CompactarEEnviarParaDropbox(string pastaOrigem, string caminhoZipDestino)
    {

        //Deleta se ja tiver um aquivo .ZIP
        File.Delete(@"/home/pedro/Documentos/My_contents.zip");
        
        // 1. Compactar a pasta
        CompactarPasta(pastaOrigem, caminhoZipDestino);

        // 2. Enviar o arquivo compactado para o Dropbox
        await EnviarParaDropbox(caminhoZipDestino);
    }

    // Exemplo de uso
    static async Task Main(string[] args)
    {
        string pastaOrigem = @"/home/pedro/Documentos/Obsidian";
        string caminhoZipDestino = @"/home/pedro/Documentos/My_contents.zip";  

        // Compactar a pasta e enviar para o Dropbox
        await CompactarEEnviarParaDropbox(pastaOrigem, caminhoZipDestino);
    }
}
