import { useEffect, useState } from "react";
import HTMLEllipsis from "react-lines-ellipsis/lib/html";
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Button, Card, Checkbox, Container, Stack, Table, TableBody, TableCell, TableContainer, TablePagination, TableRow, Typography } from "@mui/material";

import Page from "../../components/Page";
import Iconify from "../../components/Iconify";
import productService from "../../services/ProductService";
import Scrollbar from "../../components/Scrollbar";
import { fDate } from "../../utils/formatTime";
import SearchNotFound from "../../components/SearchNotFound";
import { UserListHead, UserListToolbar, UserMoreMenu } from "../../sections/@dashboard/user";

//----------------------------------------
const TABLE_HEAD = [
    { id: 'name', label: 'Name', alignRight: false },
    { id: 'price', label: 'Price', alignRight: false },
    { id: 'quantity', label: 'Quantity', alignRight: false },
    { id: 'avgRating', label: 'Average Rating', alignRight: false },
    { id: 'createdDate', label: 'Created Date', alignRight: false },
    { id: 'updatedDate', label: 'Updated Date', alignRight: false },
    { id: '' },
];

export default function ProductList() {
    const navigate = useNavigate();

    const [data, setData] = useState([]);

    const [page, setPage] = useState(1);

    const [order, setOrder] = useState('desc');

    const [selected, setSelected] = useState([]);

    const [orderBy, setOrderBy] = useState('updatedDate');

    const [totalCount, setTotalCount] = useState(0);

    const [filterName, setFilterName] = useState('');

    const [rowsPerPage, setRowsPerPage] = useState(5);

    const [useEllipsis, setUseEllipsis] = useState(true);

    useEffect(() => {
        productService.getProductList(page, rowsPerPage, `${orderBy} ${order}`, filterName).
            then(response => {
                setData(response.data);
                const { TotalCount } = JSON.parse(response.headers.pagination);
                setTotalCount(TotalCount);
            });
    }, [page, rowsPerPage, orderBy, order, filterName]);

    const handleSelectAllClick = (event) => {
        if (event.target.checked) {
            const newSelecteds = data.map((n) => n.name);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const handleClick = (event, name) => {
        const selectedIndex = selected.indexOf(name);
        let newSelected = [];
        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, name);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(selected.slice(0, selectedIndex), selected.slice(selectedIndex + 1));
        }
        setSelected(newSelected);
    };

    const handleRequestSort = (event, property) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
        console.log(property)
    };

    const handleChangePage = (event, newPage) => {
        setPage(newPage + 1);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(1);
    };

    const handleFilterByName = (event) => {
        setFilterName(event.target.value);
    };

    const handleTextClick = (e) => {
        e.preventDefault()
        setUseEllipsis(!useEllipsis);
    }

    const emptyRows = page > 1 ? Math.max(0, page * rowsPerPage - totalCount) : 0;

    const isUserNotFound = data.length === 0;

    return (
        <Page title="Product">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        Product
                    </Typography>
                    <Button variant="contained" component={RouterLink} to="../products/create" startIcon={<Iconify icon="eva:plus-fill" />}>
                        New Product
                    </Button>
                </Stack>
                <Card>
                    <UserListToolbar numSelected={selected.length} filterName={filterName} onFilterName={handleFilterByName} />
                    <Scrollbar>
                        <TableContainer sx={{ minWidth: 800 }}>
                            <Table>
                                <UserListHead
                                    order={order}
                                    orderBy={orderBy}
                                    headLabel={TABLE_HEAD}
                                    rowCount={data.length}
                                    numSelected={selected.length}
                                    onRequestSort={handleRequestSort}
                                    onSelectAllClick={handleSelectAllClick}
                                />
                                <TableBody>
                                    {data.map((row) => {
                                        // const { id, name, role, status, company, avatarUrl, isVerified } = row;
                                        const {
                                            id,
                                            name,
                                            description,
                                            price,
                                            quantity,
                                            category,
                                            avgRating,
                                            images,
                                            createdDate,
                                            updatedDate,
                                        } = row;
                                        const isItemSelected = selected.indexOf(name) !== -1;

                                        return (
                                            <TableRow
                                                hover
                                                key={id}
                                                tabIndex={-1}
                                                role="checkbox"
                                                selected={isItemSelected}
                                                aria-checked={isItemSelected}
                                            >
                                                <TableCell padding="checkbox">
                                                    <Checkbox checked={isItemSelected} onChange={(event) => handleClick(event, name)} />
                                                </TableCell>
                                                <TableCell component="th" scope="row" padding="none" 
                                                onClick={() => navigate(`../products/detail/${id}`)}
                                                style={{ cursor: 'pointer' }}>
                                                    <Stack direction="row" alignItems="center" spacing={2}>
                                                        {/* <Avatar alt={name} src={avatarUrl} /> */}
                                                        <Typography variant="subtitle2" noWrap>
                                                            {name}
                                                        </Typography>
                                                    </Stack>
                                                </TableCell>
                                                <TableCell align="left">{new Intl.NumberFormat('it-IT', { style: 'currency', currency: 'VND' }).format(price) }</TableCell>
                                                <TableCell align="left">{quantity}</TableCell>
                                                <TableCell align="left">{avgRating}</TableCell>
                                                <TableCell align="left">{fDate(createdDate)}</TableCell>
                                                <TableCell align="left">{fDate(updatedDate)}</TableCell>

                                                <TableCell align="right">
                                                    <UserMoreMenu component="products" id={id}/>
                                                </TableCell>
                                            </TableRow>
                                        );
                                    })}
                                    {emptyRows > 0 && (
                                        <TableRow style={{ height: 53 * emptyRows }}>
                                            <TableCell colSpan={6} />
                                        </TableRow>
                                    )}
                                </TableBody>

                                {isUserNotFound && (
                                    <TableBody>
                                        <TableRow>
                                            <TableCell align="center" colSpan={6} sx={{ py: 3 }}>
                                                <SearchNotFound searchQuery={filterName} />
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                )}
                            </Table>
                        </TableContainer>
                    </Scrollbar>
                    <TablePagination
                        rowsPerPageOptions={[5, 10, 25]}
                        component="div"
                        count={totalCount}
                        rowsPerPage={rowsPerPage}
                        page={page - 1}
                        onPageChange={handleChangePage}
                        onRowsPerPageChange={handleChangeRowsPerPage}
                    />
                </Card>
            </Container>
        </Page>
    )
}