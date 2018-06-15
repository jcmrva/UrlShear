namespace UrlShear

open System.Net

[<AutoOpen>]
module Config =

    type Host = | Any | Name of string | Names of string list | IPs of IPAddress list

    type QueryParams = | All | Params of string list

    type Scheme = | Https | Http | Both

    type AlphaCase = | Upper | Lower | MixedCase

    type Style = | Alpha of int * AlphaCase | AlphaNumeric of int * AlphaCase | Pattern of string

    type HostConfig = {
        original : Host
        short : Host
        removeQueryParams : QueryParams option
        scheme : Scheme
        removeFragment : bool
        expireDays : int
        style : Style
        defaultRedirect : string // if key lookup fails
    }

    type ErrorAction = | Excp | HostStyleDefault

    type ProcessOptions = {
        hostConfig : HostConfig list option
        onError : ErrorAction
    }

    let hostConfigDflt = {
            original = Any
            short = Any
            removeQueryParams = None
            scheme = Both
            removeFragment = false
            expireDays = 365
            style = AlphaNumeric (15, MixedCase)
            defaultRedirect = "" // if key lookup fails
        }

    let processOptionsDefault = {
        hostConfig = Some [ hostConfigDflt ]
        onError = HostStyleDefault
    }

    type ReqOpt = {
        style : Style option
    }
