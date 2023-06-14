const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/probability/supported-calculations",
      "/probability",
    ],
    target: "https://localhost:7121",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
