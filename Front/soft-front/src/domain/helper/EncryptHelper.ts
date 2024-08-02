import CryptoJS from 'crypto-js'

export default {

    toSha256(text: string) : string {
        return CryptoJS.SHA256(text).toString(CryptoJS.enc.Hex)
    }


}
