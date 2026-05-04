module.exports = {
    "/auth/**": {
        "target": process.env['services__api__http__0'] || "http://localhost:3000",
    }
};
