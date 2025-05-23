@cloudfoundry_scaffold
Feature: Steeltoe Configuration
  In order to show you how to use Steeltoe with IConfiguration
  You can run the configuration sample

  @net8.0
  @linux
  Scenario: Steeltoe Configuration (net8.0/linux)
    When you run: dotnet build
    And you run in the background: cf push -f manifest.yml
    And you wait until CloudFoundry app configuration-providers-sample is started
    When you get https://configuration-providers-sample/Home/ExternalConfigurationData
    Then you should see "Property bar = spam"
    When you get https://configuration-providers-sample/Home/ConfigServerSettings
    Then you should see "spring:cloud:config:name = foo"
    When you get https://configuration-providers-sample/Home/CloudFoundry
    Then you should see "vcap:application:application_name = configuration-providers-sample"
    When you get https://configuration-providers-sample/Home/PlaceholderValues
    Then you should see "ResolvedFromJson"
    And you should see "Information"

  @net8.0
  @windows
  Scenario: Steeltoe Configuration (net8.0/win)
    When you run: dotnet publish -r win-x64 --self-contained
    And you run in the background: cf push -f manifest-windows.yml -p bin/Release/net8.0/win-x64/publish
    And you wait until CloudFoundry app configuration-providers-sample is started
    When you get https://configuration-providers-sample/Home/ExternalConfigurationData
    Then you should see "Property bar = spam"
    When you get https://configuration-providers-sample/Home/ConfigServerSettings
    Then you should see "spring:cloud:config:name = foo"
    When you get https://configuration-providers-sample/Home/CloudFoundry
    Then you should see "vcap:application:application_name = configuration-providers-sample"
    When you get https://configuration-providers-sample/Home/PlaceholderValues
    Then you should see "ResolvedFromJson"
    And you should see "Information"
