import { useEffect, useState } from "react";

import categoryService from "../../services/CategoryService";

export default function CreateCategory(){

    const [selectCategory, setSelectCategory] = useState([]);

    useEffect(() => {
        categoryService.getListCategories()
        .then((response) => {
            setSelectCategory(response.data);
        })
    }, []);
    
}