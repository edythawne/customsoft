class SpinnerClass {
    spinnerNode: HTMLDivElement = document.createElement('div')
    constructor() {
        this.spinnerNode.className = 'spinner'
        this.spinnerNode.id = 'spinner-id'

    }
    show() {
        const body: HTMLBodyElement = document.getElementsByTagName('body')[0]
        body.appendChild(this.spinnerNode)
    }
    hide() {
        this.spinnerNode.remove()
    }
}
const Spinner = new SpinnerClass()
export {SpinnerClass,Spinner}
