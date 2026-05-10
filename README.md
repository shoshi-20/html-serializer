# HTML Serializer

A clean and practical C# project that fetches a live web page, parses its HTML into a structured element tree, and enables CSS-like selector queries over the result. This project demonstrates core software engineering skills including data modeling, parsing, recursion, traversal, and real-world web scraping workflows.

[![Language](https://img.shields.io/badge/language-C%23-512BD4.svg)](https://learn.microsoft.com/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/platform-.NET-blue.svg)](https://dotnet.microsoft.com/)
[![Status](https://img.shields.io/badge/status-portfolio%20project-brightgreen.svg)](https://github.com/shoshi-20/html-serializer)

## Demo

🎥 Video walkthrough: [Watch the demo](https://drive.google.com/file/d/1fwLS-TABqWAhl-fNG2j7OUK1_eDN8-WH/view?usp=drive_link)

## Overview

This project loads HTML from a target website, cleans and tokenizes the markup, builds a nested HTML element tree, and lets users query the document using selector syntax such as IDs, classes, tags, and descendant relationships.

It was built as a hands-on exercise in implementing browser-inspired document parsing and selector matching logic from scratch in C#.

## Why this project is valuable

- Implements a custom HTML serialization flow instead of relying on a full DOM library
- Models hierarchical HTML nodes with parent-child relationships
- Supports selector-based traversal over parsed elements
- Demonstrates recursive querying and tree exploration
- Uses live HTTP requests to work with real web content
- Shows practical understanding of text processing with regular expressions

## Features

- Fetches HTML from a live URL using `HttpClient`
- Removes whitespace noise to simplify parsing
- Splits raw markup into meaningful HTML tokens
- Builds an in-memory tree of `HtmlElement` objects
- Stores important metadata such as:
  - tag name
  - id
  - classes
  - attributes
  - inner HTML
  - parent and children
- Supports CSS-like selector queries including:
  - tag selectors
  - id selectors
  - class selectors
  - descendant selectors
- Traverses descendants using breadth-first search
- Prints matching elements to the console

## Example selector queries

The current implementation supports selector patterns like:

```text
#pnlMenubar td .inactBG
tr.oddrow
#mshead
img#imgMainLogo
```

## Project structure

```text
html-serializer/
├── README.md
├── html serializer.sln
└── html serializer/
    ├── Program.cs
    ├── HtmlElement.cs
    ├── HtmlHelper.cs
    ├── Selector.cs
    ├── Etentions.cs
    └── jsonFiles/
```

## Core components

### `Program.cs`
Coordinates the full workflow:
- downloads HTML from a target page
- normalizes and tokenizes content
- builds the HTML tree
- converts a selector string into a selector model
- queries matching elements and prints results

### `HtmlElement.cs`
Defines the core HTML node model used throughout the project. Each element can store:
- tag name
- id
- classes
- attributes
- inner HTML
- parent reference
- child elements

It also supports tree traversal through descendant and ancestor enumeration.

### `Selector.cs`
Parses CSS-like selector input into a structured selector chain. This enables the query engine to process nested selector relationships step by step.

### `Etentions.cs`
Contains the `QuerySelector` extension method and the recursive matching logic used to search the parsed HTML tree.

### `HtmlHelper.cs`
Loads HTML tag definitions and void tag definitions from JSON files so the parser can recognize valid elements correctly.

## How it works

1. A URL is requested with `HttpClient`
2. The returned HTML is cleaned with regular expressions
3. The HTML is split into tokens
4. A tree of `HtmlElement` objects is built while tracking parent-child relationships
5. A user selector is converted into a structured `Selector`
6. The tree is traversed recursively to find matching nodes
7. Matches are printed to the console

## Tech stack

- C#
- .NET
- `HttpClient`
- Regular Expressions
- JSON-based configuration for HTML metadata

## Getting started

### Prerequisites

- .NET SDK installed
- Internet connection for fetching target HTML pages

### Run locally

```bash
git clone https://github.com/shoshi-20/html-serializer.git
cd html-serializer
```

Open the solution in Visual Studio or run it from the command line.

```bash
dotnet run --project "html serializer/html serializer.csproj"
```

## Sample use case

This project can be used as a foundation for:
- web scraping tools
- custom HTML parsers
- selector engines
- DOM traversal exercises
- educational parsing projects

## Repository

GitHub: [shoshi-20/html-serializer](https://github.com/shoshi-20/html-serializer)

---

If you found this project interesting, feel free to explore the code and watch the demo for a full walkthrough.
