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

        (hostConfigDflt :: hosts)
        |> List.rev
        |> List.filter uriMatch 
        |> List.head

    let origModified cfgParams forceHttps removeFragment (orig:Uri) =
        let query =
            match cfgParams with
            | IncludeAll -> orig.Query
            | ExcludeAll -> String.Empty
            | Include ip -> "TODO List.filter"
            | Exclude ep -> "TODO"
        
        let bld = UriBuilder(orig)
        bld.Host <- orig.Host
        bld.Query <- query
        if forceHttps then 
            bld.Scheme <- "Https"
        if removeFragment 
            && not <| String.IsNullOrWhiteSpace(orig.Fragment) then 
            bld.Fragment <- String.Empty
        
        bld.ToString()

    let removePrefixChar (s:string) pfx = 
        if s.Substring(0, 1) = pfx then
            s.Substring(1, s.Length - 1)
        else s

    let queryToList (q:string) =
        // assumes 1 element: &param& ; 2 element: &param=value&
        let toTuple (p:string[]) =
            (p.[0], if p.Length > 1 then p.[1] else "")

        match q with
        | "" -> []
        | _ -> 
            (removePrefixChar q "?").Split([|'&'|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun e -> e.Split('=') |> toTuple)
            |> Array.toList

    let isValidName scheme (uri:Uri) =
        not uri.IsLoopback 
        && uri.IsAbsoluteUri
        && Uri.CheckHostName(uri.Host) = UriHostNameType.Dns
        && match scheme with
            | Both -> uri.Scheme = "Https" || uri.Scheme = "Http"
            | s -> uri.Scheme = s.ToString()

type Builder(cfg:HostConfig) =
    member __.Cfg = cfg

    new() = Builder(hostConfigDflt)
