export interface ILoanInfo {
    uuid: string,
    clientUuid: string,
    moneyAmount: number,
    openTime: string,
    beforeTime: string,
    creditor: ICreditorInfo
}
export interface ICreditorInfo {
    surname: string,
    name: string,
    patronymic: string
}