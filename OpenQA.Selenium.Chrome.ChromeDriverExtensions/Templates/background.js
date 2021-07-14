var config = {
    mode: "fixed_servers",
    rules: {
        singleProxy: {
            scheme: "http",
            host: "{HOST}",
            port: parseInt({PORT})
        },
        bypassList: ["foobar.com"]
    }
};

chrome.proxy.settings.set({ value: config, scope: "regular" }, function () { });

function callbackFn(details) {
    return {
        authCredentials: {
            username: "{USERNAME}",
            password: "{PASSWORD}"
        }
    };
}

chrome.webRequest.onAuthRequired.addListener(
    callbackFn,
    { urls: ["<all_urls>"] },
    ['blocking']
);