export interface ILoanInfo {
    uuid: string,
    clientUuid: string,
    moneyAmount: number,
    openTime: string,
    beforeTime: string,
    closeTime: string,
    creditor: ICreditorInfo
}
export interface ICreditorInfo {
    surname: string,
    name: string,
    patronymic: string
}

export interface IAddLoanRequest {
    moneyAmount: number,
    openTime: string,
    beforeTime: string,
    creditorSurname: string,
    creditorName: string,
    creditorPatronymic: string
}
export interface ICloseLoanRequest {
    loanUuid: string,
    closeTime: string
}