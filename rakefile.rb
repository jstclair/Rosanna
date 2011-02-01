require 'rubygems'
require 'albacore'
require 'rake/clean'

VERSION = "0.0.1.0"
OUTPUT = "build"
CONFIGURATION = 'Release'
ASSEMBLY_INFO = 'src/Rosanna/Properties/AssemblyInfo.cs'
SOLUTION_FILE = 'src/Rosanna.sln'

Albacore.configure do |config|
	config.log_level = :verbose
	config.msbuild.use :net4
end

desc "Compiles solution and runs unit tests"
task :default => [:clean, :version, :compile, :test, :publish, :package]

desc "Executes all xUnit tests"
task :test => [:xunit]

#Add the folders that should be cleaned as part of the clean task
CLEAN.include(OUTPUT)
CLEAN.include(FileList["src/**/#{CONFIGURATION}"])
CLEAN.include("TestResult.xml")

desc "Update assemblyinfo file for the build"
assemblyinfo :version => [:clean] do |asm|
	asm.version = VERSION
	asm.company_name = "Rosanna"
	asm.product_name = "Rosanna"
	asm.title = "Rosanna"
	asm.description = "The tiniest blogging engine in .NET! (A port of toto)"
	asm.copyright = "Copyright (C) Thomas Pedersen"
	asm.output_file = ASSEMBLY_INFO
	asm.com_visible = false
end

desc "Compile solution file"
msbuild :compile => [:version] do |msb|
	msb.properties :configuration => CONFIGURATION
	msb.targets :Clean, :Build
	msb.solution = SOLUTION_FILE
end

desc "Gathers output files and copies them to the output folder"
task :publish => [:compile] do
	Dir.mkdir(OUTPUT)
	Dir.mkdir("#{OUTPUT}/binaries")

	FileUtils.cp_r FileList["src/**/#{CONFIGURATION}/*.dll"].exclude(/obj\//).exclude(/.Tests/), "#{OUTPUT}/binaries"
end

desc "Executes xUnit tests"
xunit :xunit => [:compile] do |xunit|
	xunit.command = "tools/xunit/xunit.console.clr4.x86.exe"
	xunit.assemblies = FileList["src/**/#{CONFIGURATION}/*.Tests.dll"].exclude(/obj\//)
end	

desc "Creates a NuGet packaged based on the Rosanna.nuspec file"
exec :package => [:publish] do |cmd|
	Dir.mkdir("#{OUTPUT}/nuget")
	cmd.command = "tools/nuget.exe"
	cmd.parameters "pack Rosanna.nuspec -o #{OUTPUT}/nuget"
end