# Sitecore ConfigBuilder

This is an instrument to get all your Sitecore configuration files and output single summarized configuration file that will be actually used by Sitecore CMS. It also can merge it with ASP.NET sections of web.config, and also sort all XML elements to make them easier to read and compare (soring is done only when it does not change sequence of handlers, pipeline processors etc.). 

### Release Notes

#### Sitecore ConfigBuilder 1.4 was released on XXXX XX, 2015

* Added an option to download default configuration for comparison
* A number of issues were fixed
* Source code is now available for both Engine and UI

#### Sitecore ConfigBuilder 1.3 was released on June 30, 2014

* Added showconfig.xml normalization feature (it was possible only via API)
* A number of issues were fixed
* API was improved, interface and implementation was extracted from static method; all private methods became protected virtual

#### Sitecore ConfigBuilder 1.2 was released on January 16, 2014

It no longer requires actual Sitecore.Kernel.dll assembly file, now Sitecore CMS 7.0 configuration engine is embedded into the tool and API.

The API was completely simplified and requires only 1 assembly to reference. 

### Details

* It takes `web.config` file and `App_Config` folder to create `showconfig.xml` and/or `web.config.result.xml` files.
* It is like an `sitecore/admin/showconfig.aspx` page but doesn't need a Sitecore instance to be installed and run.
* The `web.config.result.xml` file is made by merging web.config and showconfig.xml files.
* It can do normalization of both files (reordering configuration sections when it doesn't change execution logic)
* The normalized versions of `web.config.result.xml` files are perfect for comparing 

### Download

The project is always available to download from [Marketplace](http://marketplace.sitecore.net/modules/sitecore_configbuilder.aspx). 

### Open Source

The sources are open for inspection by everybody, please fork and improve if you like. 

### Use its API

This repository contains only UI part of the application, the engine is distributed in a separate repository:
(https://github.com/Sitecore/Sitecore.Diagnostics.ConfigBuilder)[https://github.com/Sitecore/Sitecore.Diagnostics.ConfigBuilder]