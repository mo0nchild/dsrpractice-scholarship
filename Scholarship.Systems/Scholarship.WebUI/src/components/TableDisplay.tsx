import { Table } from "react-bootstrap"

export type TableRow = {
    color: string,
    data: any
}
export type HeaderInfo = { 
    key: string, 
    text: string,
    transform?: (value: any) => string
}
export interface TableDisplayProps {
    header: HeaderInfo[],
    content: TableRow[],
    onSelect?: (index: number) => void
}   
export default function TableDisplay(props: TableDisplayProps): React.JSX.Element {
    const { header, content, onSelect } = props;
    return (
    <Table striped bordered hover variant='dark' onSelect={(item) => {
        console.log(item)
    }}>
        <thead>
            <tr>{header.map((item, index) => <td key={index}>{item.text}</td>)}</tr>
        </thead>
        <tbody>
        { 
        content.map((item, index) => (
            <tr key={index} style={{cursor: 'pointer'}} onClick={() => onSelect?.(index)}> 
            { 
                header.map((p, n) => {
                    const value = p.transform ? p.transform(item.data[p.key]) : `${item.data[p.key]}`;
                    return <td style={{backgroundColor: item.color}} key={n}>{value}</td>
                }) 
            } 
            </tr>
        )) 
        }
        </tbody>
    </Table>
    )
}