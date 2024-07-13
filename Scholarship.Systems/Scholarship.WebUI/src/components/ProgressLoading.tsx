import { Backdrop, CircularProgress } from "@mui/material";

export interface ProgressProps {
    loading: boolean;
}

export default function ProgressLoading(props: ProgressProps): React.JSX.Element {
    const { loading } = props;
    return (
    <Backdrop open={loading} sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }} >
        <CircularProgress color="info" />
    </Backdrop>
    )
}