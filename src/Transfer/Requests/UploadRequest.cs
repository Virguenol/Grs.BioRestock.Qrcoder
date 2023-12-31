﻿using Grs.BioRestock.Shared.Enums;

namespace Grs.BioRestock.Transfer.Requests
{
    public class UploadRequest
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string UserId { get; set; }
        public UploadType UploadType { get; set; }
        public byte[] Data { get; set; }
    }
}