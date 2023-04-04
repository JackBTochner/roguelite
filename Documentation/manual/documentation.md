# Documentation

## Docfx for Unity

This project uses [Docfx for Unity](https://github.com/NormandErwan/DocFxForUnity) to automatically build code documentation using XML comments.
It is also used to format and host the website, design documentation, and manuals for implementation of mechanics within this game.

Every article uses [Github Flavored Markdown (GFM)](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/quickstart-for-writing-on-github) to format.


## Editing 

### Online
To edit an article:
1. Click the `Improve this Doc` hyperlink on the right side of the page you wish to edit.
2. Log in to a GitHub account with edit access to the pmuenjohn/roguelite repository.
3. Click the edit icon to edit each article using the GitHub text editor. 

Clicking on the Preview tab of the GitHub editor allows you to preview your work.

### Offline
All files should be in your local repository under `Documentation/manual`, edit using a text editor like VSCode with an extension that allows [GFM style Markdown previews.](https://marketplace.visualstudio.com/items?itemName=bierner.markdown-preview-github-styles)

To preview your work locally:
1. [Install DocFX](https://dotnet.github.io/docfx/index.html)
2. On a command line open in your repository root directory, run:
    ```bash
    cp README.md Documentation/index.md
    docfx Documentation/docfx.json --serve
    ```
3. The generated website will be visible at <http://localhost:8080>.

## Add new Manual entry

To add an article to the Manual, add a new Markdown file to the [`Documentation/manual`](https://github.com/pmuenjohn/roguelite/tree/main/Documentation/manual) folder in the repository and give it the .md extension. (You can do this in both the github repository or your local repository)

Do not forget to add the file you created into an entry in the [`toc.yml`](https://github.com/pmuenjohn/roguelite/blob/main/Documentation/manual/toc.yml)  file, otherwise it will not be visually accessible.