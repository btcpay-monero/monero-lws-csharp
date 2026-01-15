<div align="center">

[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/btcpay-monero/monero-lws-csharp/dotnet.yml?branch=main)](https://github.com/btcpay-monero/monero-lws-csharp/actions)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/84cf2fb05ddb4824bff5758696db0f42)](https://app.codacy.com/gh/btcpay-monero/monero-lws-csharp/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/84cf2fb05ddb4824bff5758696db0f42)](https://app.codacy.com/gh/btcpay-monero/monero-lws-csharp/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_coverage)
[![Matrix rooms](https://img.shields.io/badge/%F0%9F%92%AC%20Matrix-%23btcpay--monero-blue)](https://matrix.to/#/#btcpay-monero:matrix.org)
</div>

# Monero LWS C# Bindings

Library for interacting with Monero Light Wallet Server.

## Code formatting

We use the **unmodified** standardized `.editorconfig` from .NET SDK. Run `dotnet new editorconfig --force` to apply the latest version.

To enforce formatting for the whole project, run `dotnet format monero-lws-csharp.sln --verbosity diagnostic`

To enforce custom analyzer configuration options, we do use global _AnalyzerConfig_ `.globalconfig` file.