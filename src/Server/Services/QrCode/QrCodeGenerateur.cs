using QRCoder;

namespace Grs.BioRestock.Server.Services.QrCode
{
    public interface IQrCodeGenerateur
    {
        byte[] GenerateQrCode(string code);
    }
    public class QrCodeGenerateur : IQrCodeGenerateur
    {
        public byte[] GenerateQrCode(string code)
        {
            byte[] QRCode = new byte[0];
            if(!string.IsNullOrEmpty(code))
            {
                QRCodeGenerator qrCodeGenerateur = new QRCodeGenerator();
                QRCodeData data = qrCodeGenerateur.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode bitmap = new BitmapByteQRCode(data);
                QRCode = bitmap.GetGraphic(20);
            }
           return QRCode;
        }
    }
}
