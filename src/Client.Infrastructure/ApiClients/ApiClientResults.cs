using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Demande;
using Grs.BioRestock.Transfer.DataModels.Document;
using System;
using System.Collections.Generic;

namespace Grs.BioRestock.Client.Infrastructure.ApiClients
{
    public partial class BooleanIResult : Result<bool>
    {
    }

    public partial class BooleanResult : Result<bool>
    {
    }

    public partial class Int32Result : Result<Int32>
    {
    }

    public partial class Int32IResult : Result<Int32>
    {
    }

    public partial class StringResult : Result<string>
    {
    }

    public partial class StringIResult : Result<string>
    {
    }

    public partial class StringListIResult : Result<List<string>>
    {
    }
    public partial class DemandeSignatureDtoListResult : Result<List<DemandeSignatureDto>>
    {
    }
    public partial class DemandeSignatureDtoResult : Result<DemandeSignatureDto>
    {
    }

    public partial class DocumentDtoListResult : Result<List<DocumentDto>>
    {
    }
    public partial class DocumentDtoResult : Result<DocumentDto>
    {
    }

}