module Day04

open Common
open System
open System.Collections.Generic
open Microsoft.FSharp.Core.Operators.Checked
open Common.Operators
open System.IO
open TextCopy

let dir = __SOURCE_DIRECTORY__

//type Present = { X: int; Y: int; Z: int; }

//let parseLn (ln:string) =
//    match ln with
//    | Regex @"^(\w+) blabla (\d+)$" [a; Int b] ->
//        (a,b)
        

let findXmas (ln:string) =
    ln |> RegexMulti "XMAS" |> Seq.length

let solve1 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;
    let lns2 = lns |> List.map (fun s -> s.ToCharArray() |> Array.rev |> fun arr -> String(arr))

    let lnsVert = lns |> List.toArray |> Array.map _.ToCharArray() |> Array.transpose |> Array.map (fun arr -> String(arr)) |> List.ofArray
    let lnsVert2 = lnsVert |> List.map (fun s -> s.ToCharArray() |> Array.rev |> fun arr -> String(arr))

    let charsCoords = 
        lns |> List.toArray |> Array.map _.ToCharArray() |> Array.map (Array.indexed) |> Array.indexed 
        |> Array.map (fun (y,xs) -> xs |> Array.map (fun (x, v) -> (x,y),v))
        |> Array.concat

    let lnsDiagA =
        charsCoords |> Array.groupBy (fun ((x,y),c) -> (y-x))
        |> Array.map snd |> Array.map (Array.sortBy (fst >> fst)) |> Array.map (Array.map snd)
        |> Array.map (fun arr -> String(arr))
        |> List.ofArray
    let lnsDiagA2 = lnsDiagA |> List.map (fun s -> s.ToCharArray() |> Array.rev |> fun arr -> String(arr)) 

    let lnsDiagB =
        charsCoords |> Array.groupBy (fun ((x,y),c) -> (y+x))
        |> Array.map snd |> Array.map (Array.sortBy (fst >> fst)) |> Array.map (Array.map snd)
        |> Array.map (fun arr -> String(arr))
        |> List.ofArray
    let lnsDiagB2 = lnsDiagB |> List.map (fun s -> s.ToCharArray() |> Array.rev |> fun arr -> String(arr)) 


    let allLns = lns@lns2@lnsVert@lnsVert2@lnsDiagA@lnsDiagA2@lnsDiagB@lnsDiagB2

    let xmasCounts = allLns |> List.map findXmas |> List.sum


    let res = xmasCounts
    ClipboardService.SetText(res.ToString())
    res
 

let findMasX3 ln1 ln2 ln3 =
    let idxs1 = ln1 |> RegexMultiIdx "M.S" |> Seq.map fst |> List.ofSeq
    let idxs2 = ln2 |> RegexMultiIdx ".A." |> Seq.map fst |> List.ofSeq
    let idxs3 = ln3 |> RegexMultiIdx "M.S" |> Seq.map fst |> List.ofSeq

    let res = idxs1@idxs2@idxs3 |> List.countBy id |> List.filter (fun (_,c) -> c=3) |> List.length
    res
    

let findMasX (lns:string list) =
    lns
    |> List.windowed 3
    |> List.map (fun (s1::s2::s3::[]) -> findMasX3 s1 s2 s3)
    |> List.sum
    

let solve2 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;
    let lns2 = lns |> List.map (fun s -> s.ToCharArray() |> Array.rev |> fun arr -> String(arr))
    let lnsVert = lns |> List.toArray |> Array.map _.ToCharArray() |> Array.transpose |> Array.map (fun arr -> String(arr)) |> List.ofArray
    let lnsVert2 = lnsVert |> List.map (fun s -> s.ToCharArray() |> Array.rev |> fun arr -> String(arr))

    let xmases = [findMasX lns ; findMasX lns2 ; findMasX lnsVert ; findMasX lnsVert2]


    let res = xmases |> List.sum
    ClipboardService.SetText(res.ToString())
    res
