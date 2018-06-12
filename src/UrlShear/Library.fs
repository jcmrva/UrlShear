namespace UrlShear

open System

[<AutoOpen>]
module Create =

    let short (original:string) =
        original |> ignore
        "meep"

    let isHttp (s:string) =
        let ignoreCase = StringComparison.InvariantCultureIgnoreCase
        s.StartsWith("http:", ignoreCase) || s.StartsWith("https:", ignoreCase)

    let ssIdx (s:string) = s.IndexOf("//")

    let firstSlashIdx (s:string) = s.IndexOf("/", (ssIdx s) + 2)

    let hostName (s:string) = 
        let ss = (ssIdx s) + 2
        s.Substring(ss, (firstSlashIdx s) - ss)

    let path (s:string) =
        s.Substring((firstSlashIdx s) + 1)
