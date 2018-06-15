namespace UrlShear

open System
open System.Linq

[<AutoOpen>]
module Create =

    let hostCfg (hosts:HostConfig list) (uri:Uri) =
        let uriMatch hc = 
            match hc.original with
            | Any -> true
            | Names [] -> false
            | Names s -> s.Contains(uri.Host)
            //| IPs ips -> ips.Contains(uri.Host)
            | _ -> false
        in
        (hostConfigDflt::hosts)
        |> List.rev
        |> List.filter uriMatch 
        |> List.head


    let short (orig:Uri) (s:string) (cfg:HostConfig) =
        orig.Scheme
        + s
        + orig.Port.ToString()
        + orig.PathAndQuery
        + if not cfg.removeFragment then orig.Fragment else ""

    let isValid (uri:Uri) (cfg:HostConfig) =
        not uri.IsLoopback 
        && uri.IsAbsoluteUri
        && match cfg.scheme with
            | Both -> uri.Scheme = "Https" || uri.Scheme = "Http"
            | s -> uri.Scheme = s.ToString()
