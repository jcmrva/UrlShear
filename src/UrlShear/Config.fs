namespace UrlShear

open System.Net

[<AutoOpen>]
module Config =

    type InputHost = | Any | Names of string list | IPs of IPAddress list

    type ShortHost = | Input | InputWithPath of string | Name of string

    type QueryParams = | IncludeAll | ExcludeAll | Include of string list | Exclude of string list

    type Scheme = | Https | Http | Both

    type AlphaCase = | Upper | Lower | MixedCase

    type Style = | Alpha of int * AlphaCase | AlphaNumeric of int * AlphaCase | Pattern of string | InputText of string

    type HostConfig = {
        original : InputHost
        short : ShortHost
        filterQueryParams : QueryParams
        scheme : Scheme
        removeFragment : bool
        expireDays : int
        style : Style
        defaultRedirect : string // if key lookup fails
        forceHttps : bool
    }

    type ErrorAction = | Excp | HostStyleDefault

    type ProcessOptions = {
        hostConfig : HostConfig list option
        onError : ErrorAction
    }

    let hostConfigDflt = {
        original = Any
        short = Input
        filterQueryParams = IncludeAll
        scheme = Both
        removeFragment = false
        expireDays = 365
        style = AlphaNumeric (15, MixedCase)
        defaultRedirect = ""
        forceHttps = false
    }

    let processOptionsDefault = {
        hostConfig = Some [ hostConfigDflt ]
        onError = HostStyleDefault
    }

    type ReqOpt = {
        style : Style option
        escapedReturn : bool
    }
