module Day03

open Common
open System
open System.Collections.Generic
open Microsoft.FSharp.Core.Operators.Checked
open Common.Operators
open System.IO
open TextCopy

let dir = __SOURCE_DIRECTORY__

//type Present = { X: int; Y: int; Z: int; }

let parseLn (ln:string) =
       ln 
    |> RegexMulti @"mul\((\d+),(\d+)\)" 
    |> Seq.map (fun (ss) -> 
                       match ss with | [Int64 a; Int64 b] -> (a,b))
    |> List.ofSeq



let parseLn2 (ln:string) =
    let donts = ln |> RegexMultiIdx "don't\(\)" |> Seq.map fst |> List.ofSeq |> List.append [-2]
    let dos = ln |> RegexMultiIdx "do\(\)" |> Seq.map fst |> List.ofSeq |> List.append [-1]

    ln 
    |> RegexMultiIdx @"mul\((\d+),(\d+)\)" 
    |> Seq.choose (fun (idx, ss) -> 
            let latestDont = donts |> List.filter (fun x -> x < idx) |> List.max
            let latestDo = dos |> List.filter (fun x -> x < idx) |> List.max
            if latestDont > latestDo then None else
            match ss with | [Int64 a; Int64 b] -> Some (a,b))
    |> List.ofSeq


let solve1 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;
    
    let mults = lns |> List.map parseLn |> List.concat



    let res = mults |> List.map (fun (a,b) -> a*b) |> List.sum
    ClipboardService.SetText(res.ToString())
    res
 



let solve2 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;

    let mults = lns |> List.reduce (+) |> List.singleton |> List.map parseLn2 |> List.concat



    let res = mults |> List.map (fun (a,b) -> a*b) |> List.sum
    ClipboardService.SetText(res.ToString())


     // 89846869 too high
    res
