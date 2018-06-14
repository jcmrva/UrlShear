namespace UrlShear

open System.Net

[<AutoOpen>]
module Config =

    type Host = | Any | Names of string list | IP of IPAddress

    type QueryParams = | All | Params of string list

    type Scheme = | Https | Http | Both

    type AlphaCase = | Upper | Lower | Mixed

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
    }

    let processOptionsDefault = {
        hostConfig = None
    }

    type ReqOpt = {
        style : Style option
    }
