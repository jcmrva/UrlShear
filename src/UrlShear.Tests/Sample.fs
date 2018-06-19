module Tests

open Expecto
open UrlShear


[<Tests>]
let internals =
  testList "internals" [
    testCase "query string ? is removed" <| fun _ ->
      let query = removePrefixChar "?param=value" "?"      
      Expect.equal "param=value" query "leading ? removed"
    
    testCase "query string list to string tuple pair" <| fun _ -> 
      let qparams = "param1=val&param2&&" |> queryToList
      let expected = [
        "param1", "val"
        "param2", ""
      ]
      Expect.equal expected qparams "query string parsed"
    
  ]


[<Tests>]
let tests =
  testList "samples" [
    test "I am (shouldn't fail)" {
      "╰〳 ಠ 益 ಠೃ 〵╯" |> Expect.equal true true
    }
  ]
