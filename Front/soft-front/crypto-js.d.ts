declare module 'crypto-js' {
    const SHA256: (message: string) => { toString: (encoding: any) => string };
    const enc: {
        Hex: any;
    };
    export { SHA256, enc };
}
