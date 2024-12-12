open System
open System.Windows.Forms
open System.Drawing
open System.IO
open Newtonsoft.Json

// Dictionary storage
let mutable dictionary = Map.empty<string, string>

// File path for dictionary storage
let dictionaryFile = "dictionary.json"




