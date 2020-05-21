param(
    [ValidateSet("joystick", "points")]
    [string] $name
)

if (!$env:PSHosting) {
    $env:PSHosting = $true
    & powershell .\run.ps1 $name $args
    remove-item env:PSHosting
    exit
}

try {
    $projects = @{
        joystick = @{
            defines = @(
                #"debug"
                "info"
            )
            files = @(
                "joystick"
                "log"
                "setting"
                "parser"
                "printer"
                "maps"
                "native"
                "draggable"
                "window"
                "canvas"
                "fonts"
                "spacing"
                "positions"
                "background"
            )
            assemblies = @(
                "System"
                "System.Console"
                "System.Runtime"
                "System.Linq.Expressions"
                "System.Drawing"
                "System.Windows.Forms"
            )
            namespaces = @(
                "System"
                "System.Linq"
                "System.IO"
                "System.Text"                
                "System.Globalization"
                "System.Runtime.InteropServices"
                "System.Drawing"
                "System.Drawing.Drawing2D"
                "System.Collections.Generic"
                "System.Windows.Forms"
            )
        }
        points = @{
            files = @(
                "points"
                "window"
                "canvas" 
                "draggable"
                "fonts"
                "spacing"
            )
            assemblies = @(
                "System.Console"
                "System.Drawing"
                "System.Windows.Forms"
            )
            namespaces = @(
                "System"
                "System.Drawing"
                "System.Drawing.Drawing2D"
                "System.Drawing.Text"
                "System.Collections.Generic"
                "System.Windows.Forms"
            )
        }
    }

    if (!$projects.ContainsKey($name)) {
        write-host "project not found: $name"
        exit
    }

    $project = $projects[$name]

    $source = ""

    if ($project.defines.length -gt 0) {
        $source += "#define " + ($project.defines -join "`n#define ") + "`n"
    }

    if ($project.namespaces.length -gt 0) {
        $source += "using " + ($project.namespaces -join ";`nusing ") + ";`n"
    }

    foreach ($file in $project.files) {
        $source += "`n" + (get-content "$file.cs" -raw) + "`n"
    }

    add-type -TypeDefinition $source -ReferencedAssemblies $project.assemblies -IgnoreWarnings
    new-object $name @(,$args) | out-null
}
catch {
    if ($_.Exception.InnerException) {
        write-host $_.Exception.InnerException.Message
        write-host $_.Exception.InnerException.StackTrace
    }
    elseif ($_.Exception) {
        write-host $_.Exception.Message
        write-host $_.Exception.StackTrace
    }
    else {
        throw
    }
}