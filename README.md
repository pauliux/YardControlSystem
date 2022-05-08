In order to **create** db file, run 
```Update-Database``` command in Package Manager Console (Tools -> NuGet Package manager -> Package Manager Console)

After creating new entities, run 
```Add-Migration SomeMigrationName``` command to create migration and ```Update-Database``` to update database file in Package Manager Console (Tools -> NuGet Package manager -> Package Manager Console)
